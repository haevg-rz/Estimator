function GeneratePieChart(xvalues, yvalues) {

    var barColors = ["crimson", "cornflowerblue", "orange", "brown", "pink", "lightblue", "lightgreen", "beige", "cadetblue", "burlywood", "coral", "darkgrey"];

    new Chart("piechartdiv", {
        type: "pie",
        data: {
            labels: xvalues,
            datasets: [{
                backgroundColor: barColors,
                data: yvalues
            }]
        },
        options: {
            legend: { display: false },
        }
    });

}

function GenerateBarChart(xvalues, yvalues) {

    var barColors = ["crimson", "cornflowerblue", "orange", "brown", "pink", "lightblue", "lightgreen", "beige", "cadetblue", "burlywood", "coral", "darkgrey"];

    new Chart("barchartdiv", {
        type: "bar",
        data: {
            labels: xvalues,
            datasets: [{
                backgroundColor: barColors,
                data: yvalues
            }]
        },
        options: {
            legend: { display: false },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }],
            }
        }
    });

}

