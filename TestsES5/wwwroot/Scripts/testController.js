function testController(_questionCount, _questionIndex, _serviceUrl) {
    questionCount = _questionCount;
    questionIndex = _questionIndex;
    serviceUrl = _serviceUrl;
    var self = this;

    this.init = function (callback) {
        this.ajaxToService(serviceUrl + questionCount, callback);
        question.score = 0;
    }

    this.ajaxToService = function (url, callback) {
        var xhr = new XMLHttpRequest();
        xhr.open('Post', url + questionCount, true);
        xhr.addEventListener('load', function () {
            createNextQuestionObject();
        });
        xhr.send();
    }

    createNextQuestionObject = function () {
        var container = document.getElementById('container');
        container.innerHTML = '';
        if (questionIndex < questionCount) {
            container.innerHTML += '<h2>Вопрос ' + (questionIndex + 1) + ' из ' + questionCount + '</h2>'
            loadQuestionData(function (context) {
                var callback = function (question) {
                    self.addQuestionToList(question)
                    createNextQuestionObject();
                }
                var question = questionFactory(context.responseText);
                question.init(callback);
                questionIndex++;
            });
        }
        else {
            var result = self.questionList[0].getScore()
            showResult(result);
        }
    }

    function loadQuestionData(callback) {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/Question/GetNextQuestion/', true);
        xhr.onload = function () {
            callback(xhr);
        };
        xhr.send();
    }

    this.addQuestionToList = function (question) {
        if (self.questionList) {
            self.questionList = [];
        }
        self.questionList.push(question);
    }

    questionFactory = function (encryptedQuestion) {
        var parsingQuestion = JSON.parse(encryptedQuestion);
        var numberAnswers = parsingQuestion.answers.split("#;").length || 1;
        var text = decryptString(parsingQuestion.text);
        var timeout = parsingQuestion.timeout;
        var decryptOptions = [];
        var encryptedOptions = parsingQuestion.options.split('#;');
        for (var i = 0; i < encryptedOptions.length; i++) {
            decryptOptions.push(decryptString(encryptedOptions[i]));
        }
        if (numberAnswers === 1) {
            var answer = decryptString(parsingQuestion.answers)
            radioQuestion.prototype = new question(answer);
            var currentQuestion = new radioQuestion(decryptOptions, text, timeout);
        } else {
            var encryptedAnswers = parsingQuestion.answers.split('#;');
            var decryptedAnswers = [];
            for (var i = 0; i < encryptedAnswers.length; i++) {
                decryptedAnswers.push(decryptString(encryptedAnswers[i]));
            }
            checkboxQuestion.prototype = new question(decryptedAnswers);
            var currentQuestion = new checkboxQuestion(decryptOptions, text, timeout);
        }
        this.questionIndex++;
        return currentQuestion;
    }

    showResult = function (result) {
        var xhr = new XMLHttpRequest();
        xhr.open('Post', '/Question/Result/', true);
        xhr.send(result);
        xhr.onload = function () {
            var container = document.getElementById('container');
            container.innerHTML += xhr.responseText;
        };
    }

    function decryptString(encryptedString) {
        return decodeURIComponent(escape(window.atob(encryptedString)));
    }
}
