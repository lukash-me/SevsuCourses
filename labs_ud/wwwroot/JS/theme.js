let GetCourse = async () => {

    const url = new URL(window.location.href);
    const courseId = url.searchParams.get('id');

    try {
        const response = await fetch(`http://localhost:5036/Course/${courseId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json(); // Парсинг JSON из ответа
        return data.title; // Возвращаем результат

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

let GetThemes = async () => {
    const url = new URL(window.location.href);
    const courseId = url.searchParams.get('id');

    try {
        const response = await fetch(`http://localhost:5036/Theme/course/${courseId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json(); // Парсинг JSON из ответа
        return data; // Возвращаем результат

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}


let ShowTheme = async () => {
    let courseTitleText = await GetCourse();
    
    let courseTitleDiv = document.querySelector(".course-title");
    let courseTitle = document.createElement("span");
    courseTitle.textContent = courseTitleText;
    courseTitleDiv.appendChild(courseTitle);

    let themes = await GetThemes();
    themes.sort((a, b) => a.number - b.number);

    themes.forEach(async(element) => {
        
        let $themeTitle = $("<span>").text(element.title);
        let $image = $("<img>").attr("src", `../../images/themes_01.jpg`); //фото должны быть из бд
        let $themeText = $("<span>").addClass("text").text(element.text);
        let $imgContainer = $("<div>").addClass("img-container").append($image);
        let tasks = await GetTasks(element.id);
        let $container = $("<div>").addClass("container").append($themeTitle).append($imgContainer).append($themeText);

        let $task;
        let i = 1;
        
        tasks.forEach(elem => {
            $task = $("<span>").addClass("task").attr("id", elem.id).text(`Task №${i} - ${elem.title}`);
            $container.append($task);
            i++;
        });

        const $body = $(document.body).append($container);

        GoToTask(); //Обработчик клика на задачу
    });
    
    
}

let GetTasks = async (themeId) => {
    try {
        const response = await fetch(`http://localhost:5036/Task/theme/${themeId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json(); // Парсинг JSON из ответа
        return data; // Возвращаем результат

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

let GoToTask = () => {
    
    let $task = $(".task").on("click", function() {
        console.log("might be jump");
        window.location.href = `task.html?id=${this.id}`;
    });
}

ShowTheme();
