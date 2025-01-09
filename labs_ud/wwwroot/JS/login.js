GetStudent = async (loginValue, passwordValue) => {

    const data = {
        login: loginValue,
        password: passwordValue
    }

    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    try {
        const response = await fetch(`http://localhost:5036/Student/login`, requestOptions);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data;

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

GetMentor = async (login, password) => {
    const data = {
        login: loginValue,
        password: passwordValue
    }

    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    try {
        const response = await fetch(`http://localhost:5036/Mentor/login`, requestOptions);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data;

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

GetTeacher = async (login, password) => {
    const data = {
        login: loginValue,
        password: passwordValue
    }

    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    try {
        const response = await fetch(`http://localhost:5036/Teacher/login`, requestOptions);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data;

    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

let Login = async () => {

    const $roleElements = $(".role");
    let role;

    $roles = $(".role").on("click", function() {
        role = $(this).data("name");

        Array.from($roleElements).forEach(element => {
            $(element).removeAttr("style");
        });

        $(this).css("background", "rgb(114, 222, 255)");
    })

    const $enterButton = $("button").on("click", async function(e){
        e.preventDefault();

        login = $("input[name='login']").val();
        password = $("input[name='password']").val();
        let id;

        switch (role) {
            case "student":
                id = await GetStudent(login, password);
                break;
            case "mentor":
                id = await GetMentor(login, password);
                break;
            case "teacher":
                id = await GetTeacher(login, password);
                break;
            default:
                console.log("Не выбрано");
                break;
        }

        if(id) {
            SetCookie("id", id, 1);
            SetCookie("role", role, 1);

            window.location.href = "index.html";
        }
    });
}

SetCookie = (name, value, days) => {
    const date = new Date();
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    const expires = "expires=" + date.toUTCString();
    document.cookie = `${name}=${value}; ${expires}; path=/;`;
}

Login();