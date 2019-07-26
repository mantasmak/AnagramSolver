function requestWords() {
    let subString = document.getElementById("word-input").value;

    let url = "https://localhost:44393/api/Word?subString=" + subString;

    return fetch(url)
        .then(function (response) {
            return response.json();
        })
}

function requestAnagrams(word){
  let url = "https://localhost:44393/api/Anagram?word=" + word;

  return fetch(url)
        .then(function(response){
          return response.json();
        })
}

function renderWordList(words) {
    let content = document.getElementById('content');
    console.log(words);
    clearContent();
    let heading = document.createElement('h1');
    heading.textContent = 'Word list:';
    content.appendChild(heading);
    let list = document.createElement('div');
    list.id = 'list';
    list.classList.add('list-group');
    content.appendChild(list);
    fillTable(words);
}

function fillTable(words) {
    let listGroup = document.getElementById("list");
    let element = document.createElement('a');
    words.then(function(words){
      for (i = 0; i < words.length; i++) {
        let element = document.createElement('a');
        element.classList.add('list-group-item');
        element.classList.add('list-group-item-action');
        element.textContent = words[i];
        element.click = "renderWordList(requestAnagrams())";
        listGroup.appendChild(element);
    }
    })
}



function renderLandingPage() {
    clearContent();
    var html = 
    `<div class="row">
      <div class="col">
        <img class="logo" src="visma-logo-small.png" alt="Logo">
        <form action="../js/main.js">
          <div class="form-group">
            <label for="word">Word:</label>
            <input type="text" class="form-control" id="word-input">
          </div>
          <button onclick="renderWordList(requestWords())" type="button" class="btn btn-primary">Search</button>
        </form>
      </div>
     </div>`;
    let content = document.getElementById('content');
    content.insertAdjacentHTML('afterbegin', html);
}

function clearContent(){
  let content = document.getElementById('content');
  content.innerHTML = "";
}

