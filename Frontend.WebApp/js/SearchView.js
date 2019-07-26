class SearchView {
    constructor(controller) {
        this.controller = controller;
    }

    renderSearchPage() {
        let content = document.getElementById('content');
        let logo = this.controller.getLogo();
        let label = this.controller.getLabel();
//alert('wgg');
        var html =
            `<div class="row">
      <div class="col">
        <img class="logo" src="` + logo + `" alt="Logo">
        <form action="../js/main.js">
          <div class="form-group">
            <label for="word">` + label + `</label>
            <input type="text" class="form-control" id="word-input">
          </div>
          <button onclick="renderWordList(requestWords())" type="button" class="btn btn-primary">Search</button>
        </form>
      </div>
     </div>`;

        content.insertAdjacentHTML('afterbegin', html);
    }
}