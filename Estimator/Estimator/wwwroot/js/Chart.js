function GenerateChart(type, xvalues, yvalues) {

    var xValues = [xvalues];
    var yValues = [yvalues];
    var barColors = ["red", "green", "blue", "orange", "brown"];

    new Chart("chartdiv", {
        type: type,
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: "World Wine Production 2018"
            }
        }
    });

}

