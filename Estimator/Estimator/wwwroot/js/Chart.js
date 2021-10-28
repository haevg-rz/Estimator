function GeneratePieChart(diagramData) {

    var chart = am4core.create("chartdiv", am4charts.PieChart3D);

    chart.data = diagramData;

    var series = chart.series.push(new am4charts.PieSeries3D());
    series.dataFields.value = "estimationCount";
    series.dataFields.category = "estimationTopic";

}

