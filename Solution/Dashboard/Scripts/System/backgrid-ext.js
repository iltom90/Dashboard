// File di Estensioni per la Backgrid

// Estendo la classe "Cell" per aggiungere alle colonne ulteriori prorietà
var originalCellInitialize = Backgrid.Cell.prototype.initialize;
Backgrid.Cell.prototype.initialize = function (options) {
    originalCellInitialize.apply(this, arguments);

    // Width
    var width = options.column.get("width");
    if (width) {
        this.$el.css("white-space", "normal");
        this.$el.css("width", width);
    }

    // Align
    var align = options.column.get("align");
    if (align) {
        this.$el.css("text-align", align);
    }
};

// Estendo la classe "Cell" per aggiungere alle colonne ulteriori prorietà
var originalHeaderCellInitialize = Backgrid.HeaderCell.prototype.initialize;
Backgrid.HeaderCell.prototype.initialize = function (options) {
    originalHeaderCellInitialize.apply(this, arguments);

    // Width
    var width = options.column.get("width");
    if (width) {
        this.$el.css("white-space", "normal");
        this.$el.css("width", width);
    }

    // Align
    var align = options.column.get("align");
    if (align) {
        this.$el.css("text-align", align);
    }
};