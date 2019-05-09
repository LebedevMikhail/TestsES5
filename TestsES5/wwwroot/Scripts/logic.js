
function start() {
    document.getElementById('startButton').setAttribute('hidden', 'true');
    var controller = new testController(_questionCount = 3, _questionIndex = 0, _serviceUrl = 'Question/TestInit/');
    controller.init();
}
