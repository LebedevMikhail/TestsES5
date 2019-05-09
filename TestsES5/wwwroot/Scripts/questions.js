function question(_answers, _options, _text) {
    var answers = _answers;
    this.options = _options;
    this.text = _text;

    if (question.score) {
        question.score = 0;
    }

    function incrementScore() {
        question.score++;
    }

    this.getScore = function () {
        return question.score;
    }


    this.handleNext = function (userAnswers) {
        if (compareTo(userAnswers, answers)) {
            incrementScore();
        }
    }

    function compareTo(arr1, arr2) {
        if (!(arr2 instanceof window.Array)) {
            arr2 = arr2.split() || arr2;
        }
        arr1 = arr1.map(obj => JSON.stringify(obj)).sort();
        arr2 = arr2.map(obj => JSON.stringify(obj)).sort();
        return (JSON.stringify(arr1) === JSON.stringify(arr2));
    }
}


function radioQuestion(options, text, timeout) {
    var self = this;
    this.init = function (callback) {
        var div = document.createElement('div');
        div.style = 'margin: 3px auto; text-align: left; ';
        div.className = 'question';
        div.innerHTML += '<p>' + text + '</p>';
        for (var i = 0; i < options.length; i++) {
            div.innerHTML += '<div>'
            div.innerHTML += '<input class ="inputs" name="option" type="radio" value=\"' + options[i] + '\">' + options[i];
            div.innerHTML += '</div>'
        }
        div.innerHTML += '<div><input class="btn btn-success" type="submit" id="answerButton" value="Ответить"></div>'
        var container = document.getElementById('container');
        container.appendChild(div);
        var answerButton = document.getElementById('answerButton');
        if (timeout != null || timeout != undefined) {
            var timer = setTimeout(function () {
                alert('Время для ответа на этот вопрос вышло');
                answerButton.click();
            }, timeout * 1000);
        }
        answerButton.onclick = function () {
            clearTimeout(timer);
            handleNext();
            callback(self);
        }
    }

    function handleNext() {
        var inputs = document.getElementsByClassName('inputs');
        var userAnswers = [];
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                userAnswers.push(inputs[i].value);
            }
        }
        self.handleNext(userAnswers);
    }
}


function checkboxQuestion(options, text, timeout) {
    var self = this;
    this.init = function (callback) {
        var div = document.createElement('div');
        div.className = 'question';
        div.style = 'margin-left: auto; margin-right: auto; text-align: left; ';
        div.innerHTML += '<p>' + text + '</p>';
        for (var i = 0; i < options.length; i++) {
            div.innerHTML += '<div>'
            div.innerHTML += '<input class="inputs" type="checkbox" style="margin: 0px auto; text-align: left;" name=\"option' + i
                + '\" value=\'' + options[i] + '\' >' + options[i];
            div.innerHTML += '</div>'
        }
        div.innerHTML += '<div><input class="btn btn-success" type="submit" id="answerButton" value="Ответить"></div>'
        var container = document.getElementById('container');
        container.appendChild(div);
        var answerButton = document.getElementById('answerButton');
        if (timeout != null || timeout != undefined) {
            var timer = setTimeout(function () {
                alert('Время для ответа на этот вопрос вышло');
                answerButton.click();
            }, timeout * 1000);
        }
        answerButton.onclick = function () {
            clearTimeout(timer);
            handleNext();
            callback(self);
        }
    }

    function handleNext() {
        var inputs = document.getElementsByClassName('inputs');
        var userAnswers = [];
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                userAnswers.push(inputs[i].value);
            }
        }
        self.handleNext(userAnswers);
    }
}

