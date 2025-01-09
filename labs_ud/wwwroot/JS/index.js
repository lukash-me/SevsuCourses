
let GetAllCourses = () => {

    fetch('http://localhost:5036/Course')
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.json(); // Парсинг JSON из ответа
    })
    .then(data => {
        ShowAllCourses(data);
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });

}

GetAllCourses();

let ShowAllCourses = (data) => {

    let container = document.querySelector(".container");

    let inRow = 0;
    let cardsRow = document.createElement("div");
    cardsRow.setAttribute("class", "cards-row");

    data.forEach(element => {

        if (inRow === 4){
            container.appendChild(cardsRow);
            cardsRow = document.createElement("div");
            cardsRow.setAttribute("class", "cards-row");
            inRow = 0;
        }

        let title = document.createElement("h1");
        title.textContent = element.title;
        let description = document.createElement("span");
        description.textContent = element.description;
        let cardText = document.createElement("div");
        cardText.setAttribute("class", "card-text");
        cardText.appendChild(title);
        cardText.appendChild(description);

        let image = document.createElement("img");
        
        //image.setAttribute("src", `../../images/${element.photo}`);
        image.setAttribute("src", `../../images/abstract_02.jpg`); //заменить динамически
        let imageDiv = document.createElement("div");
        imageDiv.setAttribute("class", "image");
        imageDiv.appendChild(image);

        let card = document.createElement("div");
        card.setAttribute("class", "course-card");
        card.setAttribute("data-id", element.id);
        
        card.appendChild(imageDiv);
        card.appendChild(cardText);

        cardsRow.appendChild(card);

        inRow++;
    });

    if(inRow === 4){
        container.appendChild(cardsRow);
        cardsRow = document.createElement("div");
        cardsRow.setAttribute("class", "cards-row");
    }
    
    let addCard = document.createElement("div");
    let addButton = document.createElement("div");
    addCard.setAttribute("class", "course-add-card");
    addButton.setAttribute("class", "add-button");
    addCard.appendChild(addButton);

    cardsRow.appendChild(addCard);
    
    container.appendChild(cardsRow);
}

let courseClick = () => {

    let container = document.querySelector(".container");
    container.addEventListener('click', (event) => {

        const card = event.target.closest('.course-card');
        const addCard = event.target.closest('.course-add-card');
            if (card) {
                window.location.href = `theme.html?id=${card.dataset.id}`;
            }
            
            if (addCard) {
                console.log("yeah");
            }
    })
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

courseClick();