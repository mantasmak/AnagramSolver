function requestWords(){
    let subString = document.getElementById("word-input").value;

    let url = "https://localhost:44393/api/Word?subString=" + subString;

    fetch(url)
    .then(function(response){
        return response.json();
    })
    .then(function(myJson){
        console.log(JSON.stringify(myJson));
    })
}

function fillTable(){
    let listGroup = document.getElementById("list");
    let element = document.createElement('a');
    element.classList.add('list-group-item');
    element.classList.add('list-group-item-action');
    element.textContent = 'qegqw3tg2rt4';
    listGroup.appendChild(element);
}