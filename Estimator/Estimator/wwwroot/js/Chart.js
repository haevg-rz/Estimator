function GeneratePieChart(xvalues, yvalues) {

    var xValues = xvalues;
    var yValues = yvalues;
    var barColors = ["red", "green", "blue", "orange", "brown"];

    new Chart("piechartdiv", {
        type: "pie",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            legend: { display: false },
        }
    });

}

function GenerateBarChart(xvalues, yvalues) {

    var xValues = xvalues;
    var yValues = yvalues;
    var barColors = ["red", "green", "blue", "orange", "brown"];

    new Chart("barchartdiv", {
        type: "bar",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            legend: { display: false },
        }
    });

}

