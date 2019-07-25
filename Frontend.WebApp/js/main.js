function requestWords() {
    let subString = document.getElementById("word-input").value;

    let url = "https://localhost:44393/api/Word?subString=" + subString;

    return fetch(url)
        .then(function (response) {
            return response.json();
        })
}

function renderWordList() {
    let content = document.getElementById('content');
    content.innerHTML = '';
    let heading = document.createElement('h1');
    heading.textContent = 'Word list:'
    content.appendChild(heading);
    let list = document.createElement('div');
    list.id = 'list';
    list.classList.add('list-group');
    content.appendChild(list);
    fillTable(requestWords());
}

function fillTable(words) {
    let listGroup = document.getElementById("list");
    let element = document.createElement('a');
    for (i = 0; i < words.length; i++) {
        let element = document.createElement('a');
        element.classList.add('list-group-item');
        element.classList.add('list-group-item-action');
        element.textContent = words[i];
        listGroup.appendChild(element);
    }
}

function renderLandingPage() {
    /* let content = document.getElementById('content');
    let row = document.createElement('div');
    row.classList.add('row');
    row.classList.add('vertical-center');
    content.appendChild(row);
    let column = document.createElement('div');
    column.classList.add('col');
    row.appendChild(column);
    let image = document.createElement('img');
    image.classList.add('logo');
    image.src = 'visma-logo-small.png';
    image.alt = 'Logo';
    column.appendChild(image);
    let form = document.createElement('form');
    form.action = '#';
    column.appendChild(form);
    let formGroup = document.createElement('div');
    formGroup.classList.add('form-group');
    formGroup.classList.add('custom-text-input');
    form.appendChild(formGroup);
    let button = document.createElement('button');
    button.addEventListener('click', function(){renderWordList()});
    button.type = 'submit';
    button.classList.add('btn');
    button.classList.add('btn-primary');
    button.textContent = 'Search';
    form.appendChild(button);
    let label = document.createElement('label');
    label.textContent = 'Word:';
    formGroup.appendChild(label);
    let input = document.createElement('input');
    input.type = 'text';
    input.classList.add('form-control');
    input.id = 'word-input';
    formGroup.appendChild(input); */
    var html = `<div class="row vertical-center">
    <div class="col">
      <img class="logo" src="visma-logo-small.png" alt="Logo">
      <form action="../js/main.js">
        <div class="form-group custom-text-input">
          <label for="word">Word:</label>
          <input type="text" class="form-control" id="word-input">
        </div>
        <button onclick="renderWordList()" type="button" class="btn btn-primary">Search</button>
      </form>
    </div>
  </div>
</div>`
    let content = document.getElementById('content');
    content.insertAdjacentHTML('afterbegin', html);
}