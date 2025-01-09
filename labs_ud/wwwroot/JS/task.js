let GetTask = async (taskId) => {

    try {
        const response = await fetch(`http://localhost:5036/Task/${taskId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json(); // Парсинг JSON из ответа
        return data; // Возвращаем результат

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

let GetThemeData = async (themeId) => {

    try {
        const response = await fetch(`http://localhost:5036/Theme/${themeId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json(); // Парсинг JSON из ответа
        return [data.title, data.number]; // Возвращаем результат

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

let GetAnswer = async (taskId, studentId) => {

    try {
        const response = await fetch(`http://localhost:5036/Answer/taskId=${taskId}&studentId=${studentId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json(); // Парсинг JSON из ответа
        return data; // Возвращаем результат

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

let SendAnswerText = async (answerId, textValue) => {
     
    const data = {
        text: textValue
    }

    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    try {
        const response = await fetch(`http://localhost:5036/Answer/${answerId}/answerText`, requestOptions);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data;

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

let ShowTask = async () => {

    const url = new URL(window.location.href);
    const taskId = url.searchParams.get('id');

    const userId = GetCookie("id");

    let task = await GetTask(taskId);
    let themeData = await GetThemeData(task.themeId);

    let answer = await GetAnswer(taskId, userId);

    const $themeTitle = $("<h1>").text(`Theme №${themeData[1]+1}. ${themeData[0]}`);
    const $themeString = $("<div>").addClass("theme-title").append($themeTitle);

    //Блок с картинкой
    const $image = $("<img>").attr("src", "../../images/themes_01.jpg");
    const $imageContainer = $("<div>").addClass("img-container").append($image);
    const $taskTitle = $("<h2>").text(task.title);

    //Блок с условием задачи
    const $conditionTitle = $("<h2>")
    .text("Условие");

    const $conditionTitleContainer = $("<div>")
    .addClass("title")
    .append($conditionTitle);

    const $conditionText = $("<span>")
    .text(task.condition);

    const $conditionContainer = $("<div>")
    .addClass("container")
    .append($conditionTitleContainer)
    .append($conditionText);

    //Кнопка Отправки ответа
    const $buttonAnswer = $("<button>")
    .attr("id", "#toModalAnswer")
    .text("Ответить")
    .on("click", function() {
        const $toTaskButton = $("<button>")
        .text("Вернуться к задаче")
        .on("click", function(){
            $shadowAnswer.remove();
        });

        const $sendAnswer = $("<button>")
        .text("Отправить")
        .on("click", async function(){
            await SendAnswerText(answer.id, $answerTextArea[0].value);
            $("#answer").empty();
            answer = await GetAnswer(taskId, userId);
            SetStudentAnswerBlock($("#answer"), answer);
            $shadowAnswer.remove();
        });

        const $modalAnswerButtonsContainer = $("<div>")
        .addClass("button-container")
        .append($toTaskButton)
        .append($sendAnswer);

        const $answerTextArea = $("<textarea>")
        .attr("placeholder", "Введите текст");

        const $textSpan = $("<span>")
        .text("Введите текст ответа");

        const $modalAnswer = $("<div>")
        .addClass("modal")
        .append($textSpan)
        .append($answerTextArea)
        .append($modalAnswerButtonsContainer);

        const $shadowAnswer = $("<div>")
        .addClass("shadow")
        .append($modalAnswer);

        $body.append($shadowAnswer);
    });

    const $buttonContainer = $("<div>")
    .addClass("button-container")
    .append($buttonAnswer);

    //Блок ответ студента
    let $answerContainer = $("<div>")
    .addClass("container")
    .attr("id", "answer");

    SetStudentAnswerBlock($answerContainer, answer);

    //Кнопка оценки ответа
    const $markButton = $("<button>")
    .attr("id", "#toModalReply")
    .text("Оценить работу");

    const $markButtonContainer = $("<div>")
    .addClass("button-container")
    .append($markButton);

    //Блок ответа ментора
    const $replyTitle = $("<h2>").text("Ответ ментора");
    const $replyTittleContainer = $("<div>").addClass("title").append($replyTitle);
    const $replyText = $("<span>").text(answer.replyText);
    const $replyContainer = $("<div>")
    .addClass("container")
    .append($replyTittleContainer)
    .append($replyText);

    //Кнопки переходов
    const $prevTaskButton = $("<button>")
    .attr("id", "#prevTask")
    .text("Предыдущее задание");

    const $nextTaskButton = $("<button>")
    .attr("id", "nextTask")
    .text("Следующее задание");

    const $taskNavButtonsContainer = $("<div>")
    .addClass("button-container")
    .append($prevTaskButton)
    .append($nextTaskButton);

    const $toThemesButton = $("<button>")
    .attr("id", "#toThemes")
    .text("Вернуться к темам");

    const $toThemesButtonContainer = $("<div>")
    .addClass("button-container")
    .append($toThemesButton);

    //Добавление блоков в верстку
    const $block = $("<div>").addClass("block")
    .append($taskTitle)
    .append($imageContainer)
    .append($conditionContainer)
    .append($buttonContainer)
    .append($answerContainer)
    .append($markButtonContainer)
    .append($replyContainer)
    .append($taskNavButtonsContainer)
    .append($toThemesButtonContainer);

    const $body = $(document.body)
    .append($themeString)
    .append($block);


}

SetStudentAnswerBlock = ($container, answer) => {
    const $answerTitle = $("<h2>").text("Ответ студента");
    const $answerTitleContainer = $("<div>").addClass("title").append($answerTitle);
    const $answerText = $("<span>").text(answer.answerText);
    const $answerStatus = $("<div>").addClass("status").text("Ожидает проверки / оценка");
    
    $container
    .append($answerTitleContainer)
    .append($answerText)
    .append($answerStatus);
}

GetCookie = (nameValue) => {
    const name = nameValue + "=";
    const cookiesArray = document.cookie.split(';');
    for (let i = 0; i < cookiesArray.length; i++) {
        let cookie = cookiesArray[i];
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(name) === 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }
    return null;
}

console.log(GetCookie("id"));


ShowTask();