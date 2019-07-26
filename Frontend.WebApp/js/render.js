(function (){
    let model = new SearchModel();
    let controller = new SearchController(model);
    let view = new SearchView(controller);
    view.renderSearchPage();
})();