$(document).ready(function () {
    // create grid and load data
    CreateGridWorks();   
});

var cellAlignRight = Backgrid.Cell.extend({
    className: 'cell-align-right'
});

function CreateGridWorks() {
    // create backbone model for frid
    var mGrid = Backbone.Model.extend({});

    // layout columns
    var columns = [
        {
            name: "submittedAnswerId",
            label: "Answer ID",
            editable: false,
            sortable: false,
            cell: cellAlignRight,
            width: "80px"
        }, {
            name: "submitDateTime",
            label: "Submit Date",
            editable: false,
            sortable: true,
            cell: Backgrid.Extension.MomentCell.extend({
                modelFormat: "DD/MM/YYYY hh:mm:ss",
                displayLang: "it-it",
                displayFormat: "DD/MM/YYYY",
                className: "cell-align-center"
            }),
            formatter: _.extend({}, Backgrid.CellFormatter.prototype, {
                fromRaw: function (rawValue, model) {
                    if (rawValue == '' || rawValue == null) {
                        return '';
                    } else {
                        var data = new Date(parseInt(rawValue.substr(6)));
                        return ParseDateToIT(data);
                    }
                }
            }),
            width: "80px"
        }, {
            name: "correct",
            label: "Correct",
            editable: false,
            sortable: true,
            cell: Backgrid.BooleanCell.extend({
                className: 'grid_pulsanti',
                formatter: _.extend({}, Backgrid.CellFormatter.prototype, {
                    fromRaw: function (rawValue, model) {
                        if (model.attributes["correct"] == 1) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }),
            }),
            width: "100px"
        }, {
            name: "progress",
            label: "Progress",
            editable: false,
            sortable: false,
            cell: "string",
            width: "150px"
        },
        {
            name: "userId",
            label: "User Id",
            editable: false,
            sortable: true,
            cell: "string",
            width: "150px"
        }, {
            name: "exerciseId",
            label: "Exercise Id",
            editable: false,
            sortable: false,
            cell: "string",
            width: "150px"
        }, {
            name: "difficulty",
            label: "Difficulty",
            editable: false,
            sortable: true,
            cell: "string",
            width: "150px"
        },
        {
            name: "subject",
            label: "Subject",
            editable: false,
            sortable: false,
            cell: "string",
            width: "150px"
        },
        {
            name: "Domain",
            label: "Exercise Id",
            editable: false,
            sortable: false,
            cell: "string",
            width: "150px"
        },
        {
            name: "learningObjective",
            label: "Learning Objective",
            editable: false,
            sortable: false,
            cell: "string",
            width: "150px"
        }];

    // create paginable collection
    var PageableGrid = Backbone.PageableCollection.extend({
        model: mGrid,
        url: global_pathGetData,
        state: {
            pageSize: 10,
            sortKey: "submitDateTime",
            order: 2
        },
        queryParams: {
            totalPages: null,
            totalRecords: null
        },
        parseState: function (resp, queryParams, state, options) {
            return {
                totalPages: resp.NumTotPages,
                totalRecords: resp.NumTotWorks
            };
        },
        parseRecords: function (resp, options) {
            // set title text
            $('#hTitolo').html('<span style="font-weight: bold; color: #428bca;">' + resp.NumTotWorks + '</span> Works');

            // bind data
            return resp.Works;
        }
    });
    var pageableGrid = new PageableGrid();
    // create grid
    var grid = new Backgrid.Grid({
        columns: columns,
        collection: pageableGrid
    });    
    grid.collection.on('backgrid:sort', function (model) {
        var cid = model.cid;
        var filtered = model.collection.filter(function (model) {
            return model.cid !== cid;
        });
        _.each(filtered, function (model) {
            model.set('direction', null);
        });
    });
    $("#grid").append(grid.render().el);
    // create paginator
    var paginator = new Backgrid.Extension.Paginator({
        collection: pageableGrid
    });
    $("#grid").after(paginator.render().el);

    // crate filter
    var filter_generic = new Backgrid.Extension.ServerSideFilter({
        collection: pageableGrid,
        name: 'Filter'
    });
    $("#filterDiv").append(filter_generic.render().el);

    // bind grid
    pageableGrid.fetch({ reset: true });


    // filter
    var timer = null;
    $('#txtFilterSubject').keydown(function () {
        clearTimeout(timer);
        timer = setTimeout(doStuff, 500)
    });
    $('#txtFilterUserID').keydown(function () {
        clearTimeout(timer);
        timer = setTimeout(doStuff, 500)
    });
}

function doStuff() {
    ActiveFilter();
}

var valActiveFilter;

function ActiveFilter(over_generic) {

    var filterSubject = "SUB=" + $('#txtFilterSubject').val();
    var filterUser = "USR=" + $('#txtFilterUserID').val();

    if (over_generic != undefined && over_generic != null) {
        filterSubject = "SUB=" + over_generic;
    }


    valActiveFilter = filterSubject + "|" + filterUser;
    $('input[name=Filter]').val(valActiveFilter);
    setTimeout('ChkActiveFilter()', 300);
}

function ChkActiveFilter() {
    var filterSubject = "SUB=" + $('#txtFilterSubject').val();
    var filterUser = "USR=" + $('#txtFilterUserID').val();

    if (valActiveFilter == filterSubject + "|" + filterUser) {
        $('input[name=Filter]').closest("form").submit();
    }
}

function ParseDateToIT(data) {
    if (data == null || data == undefined) {
        return null;
    }
    return PadLeft(FormatNumCifre(data.getDate(), 2),'0',2) + "/"
        + PadLeft(FormatNumCifre(data.getMonth() + 1, 2), '0', 2) + "/"
        + FormatNumCifre(data.getFullYear(), 4) + " "
        + PadLeft(FormatNumCifre(data.getHours(), 2), '0', 2) + ":"
        + PadLeft(FormatNumCifre(data.getMinutes(), 2), '0', 2);
}

function FormatNumCifre(val, num) {
    var result = val;
    while (result.toString().indexOf(" ") > -1) {
        result = result.replace(" ", "");
    }
    while (result.length < num) {
        result = "0" + result;
    }
    return result;
}

function PadLeft(val, char, num) {
    var result = val;    
    for (var idx = 1; idx <= num; idx++) {
        if (result.toString().length < num) {
            result = char + result;
        }
    }
    
    return result;
}