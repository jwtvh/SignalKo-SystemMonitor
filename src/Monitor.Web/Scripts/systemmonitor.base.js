
function MachineStateViewModel(MachineName) {
    var self = this;
    self.MachineName = MachineName;

    self.ChartTitle = MachineName;
    self.ChartContainerId = "chart-" + MachineName;
    self.ChartContainer = ko.observable("<div id='" + self.ChartContainerId + "'></div>");

    self.ChartSizeCurrent = 0;
    self.ChartSizeMax = 60;
    self.CaptureStartTime = new Date();

    self.getSecondsSinceMidnight = function(datetime)
    {
        // get todays midnight
        var midnight = new Date()
        midnight.setHours(0);
        midnight.setMinutes(0);
        midnight.setSeconds(0);
        midnight.setMilliseconds(0);

        // cut off milliseconds
        datetime.setMilliseconds(0);

        // calculate difference
        var millisecondsTillMidnight = midnight.getTime();
        var millisecondsOfSuppliedDateTime = datetime.getTime();
        var millisecondsSinceMidnight = millisecondsOfSuppliedDateTime - millisecondsTillMidnight;

        // return seconds since midnight
        return millisecondsSinceMidnight / 1000;
    };

    self.getFormattedTime = function(dateTime) {
        var hours = dateTime.getHours();
        var minutes = dateTime.getMinutes();
        var seconds = dateTime.getSeconds();

        var formatTimeComponent = function(number) {
            if (number < 10) {
                return "0" + number;
            }

            return number;
        };

        return formatTimeComponent(hours) + ":" + formatTimeComponent(minutes, 2) + ":" + formatTimeComponent(seconds);
    };

    self.getAbsoluteTimestampFromRelativeChartPosition = function(chartPosition) {
        var secondsSinceCaptureStart = chartPosition;
        var millisecondsSinceCaputureStart = secondsSinceCaptureStart * 1000;
        var millisecondsAtCaptureStart = self.CaptureStartTime.getTime();

        var timestamp = new Date(millisecondsAtCaptureStart + millisecondsSinceCaputureStart);
        return timestamp;
    };

    self.chart = null;
    var initializeChart = function() {
        self.chart = new Highcharts.Chart({
            chart: {
                renderTo: self.ChartContainerId,
                type: 'line',
                zoomType: 'x',
                marginRight: 130,
                marginBottom: 25
            },
            title: {
                text: self.ChartTitle,
                x: -20 //center
            },
            xAxis: {
                type: 'linear',
                labels: {
                    formatter: function() {
                        var timestamp = self.getAbsoluteTimestampFromRelativeChartPosition(this.value);
                        return self.getFormattedTime(timestamp);
                    }
                }
            },

            yAxis: {
                title: {
                    text: '%'
                },
                min: 0,
                max: 100
            },
            tooltip: {
                formatter: function() {
                    var timestamp = self.getAbsoluteTimestampFromRelativeChartPosition(this.x);
                    var value = Highcharts.numberFormat(this.y, 2);

                    return '<b>'+ this.series.name +'</b><br/>'+
                    self.getFormattedTime(timestamp) +'<br/>'+
                    value;
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -10,
                y: 100,
                borderWidth: 0
            },
            plotOptions: {
                line: {
                    lineWidth: 1,
                    marker: {
                        enabled: false,
                        states: {
                            hover: {
                                enabled: false
                            }
                        }
                    },
                    shadow: false,
                    states: {
                        hover: {
                            lineWidth: 1
                        }
                    }
                }
            },
            series: [{
                name: "CPU Utilization in %",
                data: [self.getSecondsSinceMidnight(new Date())]
            }]
        });
    };

    self.GetOrAddSeries = function(seriesName) {
        var series = self.chart.series;

        for (var i = 0; i < series.length; i++)
        {
            var entry = series[i];
            if (entry.name === seriesName)
            {
                return entry;
            }
        }

        self.chart.addSeries({ "name": seriesName, "data": []});
    };

    self.AddData = function(Name, Timestamp, Value) {
        if (self.chart === null) {
            initializeChart();
        }

        var series = self.GetOrAddSeries(Name);

        var secondsSinceMidnight = self.getSecondsSinceMidnight(new Date(Timestamp));
        var secondsBetweenMidnightAndCaptureStart = self.getSecondsSinceMidnight(self.CaptureStartTime);

        var x = secondsSinceMidnight - secondsBetweenMidnightAndCaptureStart;
        var y = parseFloat(Value);

        var shift = !(self.ChartSizeCurrent < self.ChartSizeMax);

        if (x >= 0 && x <= 86400) {
            series.addPoint([x, y], false, shift);
            self.ChartSizeCurrent++;
        }        
    };

    self.UpdateChart = function() {
        self.chart.redraw();
    };
}

function MachineStatesViewModel() {
    var self = this;
    self.machineStateViewModels = ko.observableArray();

    self.GetMachineStateViewModel = function (MachineName) {
        var vms = self.machineStateViewModels();
        
        for (var i = 0; i < vms.length; i++) {
            var vm = vms[i];
            if (vm.MachineName === MachineName) {
                return vm;
            }
        }
        
        return null;
    };
    
    var hub = $.connection.systemInformationHub;

    $.extend(hub, {
        displaySystemStatus: function(systemStatus)
        {
            var machineStateModel = self.GetMachineStateViewModel(systemStatus.MachineName);

            if (machineStateModel === null)
            {
                machineStateModel = new MachineStateViewModel(systemStatus.MachineName);
                self.machineStateViewModels.push(machineStateModel);
            }

            for (var i = 0; i < systemStatus.DataPoints.length; i++)
            {
                var dataPoint = systemStatus.DataPoints[i];
                machineStateModel.AddData(dataPoint.Name, systemStatus.Timestamp, dataPoint.Value);
            }

            machineStateModel.UpdateChart();
        }
    });

    $.connection.hub.start(); 
   
}

ko.applyBindings(new MachineStatesViewModel());