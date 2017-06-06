$(document).ready(function () {
    // load chart in oder to the value selected
    $("#lstUserID").change(function () {
        LoadUserIDChart(this.value);
    });

    LoadChartAnswerState("chartAnswerStateSub1", "Begrijpend Lezen")
    LoadChartAnswerState("chartAnswerStateSub2", "Rekenen")
    LoadChartAnswerState("chartAnswerStateSub3", "Spelling")
});

function LoadChartAnswerState(chartID, subject) {
    $.post(global_PathGetAnswerStateChartData, { Subject: subject }
    , function (resp) {
        if (resp.length > 0) {

            // load data
            Highcharts.chart(chartID, {
                chart: {
                    type: 'pie'
                },
                title: {
                    text: subject
                },
                subtitle: {
                    text: ''
                },
                plotOptions: {
                    series: {
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}: {point.y:.1f}%'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '',
                    pointFormat: '{point.name}: <b>{point.y} <i>({point.percentage:.1f}%)</i></b>'
                },
                    series: [{
                    type: 'pie',
                    name: 'Answer State',
                    data: resp
                }]                
            });

            
            
        }
    }
    , 'json').fail(function (xhr, status, error) {
        bootbox.alert('Errors eccurs');
    });
}

function LoadUserIDChart(userId) {

    if (userId != "*") {
        $.post(global_PathGetUserIdChartData, { UserID: userId }
        , function (resp) {
            if (resp.length > 0) {

                // load data
                var arrSerie = [];

                arrSerie.push({
                    name: 'Correct',
                    data: resp[0],
                    dataLabels: {
                        enabled: true,
                        rotation: -90,
                        color: '#FFFFFF',
                        align: 'right',
                        x: 4,
                        y: 10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 3px black'
                        }
                    }
                });
                arrSerie.push({
                    name: 'Wrong',
                    data: resp[1],
                    dataLabels: {
                        enabled: true,
                        rotation: -90,
                        color: '#FFFFFF',
                        align: 'right',
                        x: 4,
                        y: 10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 3px black'
                        }
                    }
                });

                $('#chartUserChart').highcharts({

                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Answers for User: ' + userId
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        type: 'category',
                        labels: {
                            rotation: -45,
                            style: {
                                fontSize: '13px',
                                fontFamily: 'Verdana, sans-serif'
                            }
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Correct answer'
                        }
                    },
                    legend: {
                        enabled: true
                    },
                    tooltip: {
                        headerFormat: '',
                        pointFormat: '<b>{point.y}</b> {series.name} answer for <b>{point.name}</b>',
                    },
                    series: arrSerie,
                    exporting: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    }
                });
            }
        }
        , 'json').fail(function (xhr, status, error) {
            bootbox.alert('Errors eccurs');
        });
        $('#chartUserChart').show();
    }
    else {
        $('#chartUserChart').hide();
    }
}



