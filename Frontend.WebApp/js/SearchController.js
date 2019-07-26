class SearchController{
    constructor(model){
        this.model = model;
    }

    getLogo(){
        return this.model.image;
    }

    getLabel(){
        return this.model.label;
    }
}