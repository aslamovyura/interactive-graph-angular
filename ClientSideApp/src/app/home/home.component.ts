import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Label, Color } from 'ng2-charts';
import { SalesService, AlertService } from '../_services';
import { SaleStatistic } from '../_models';
import { AppConstants } from '../_constants';

@Component({
    selector: 'home-app',
    templateUrl: './home.component.html',
    styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit {

    startDate: Date;
    endDate: Date;
    scale: number;
    saleStatistic: SaleStatistic;
    isLoading: boolean;
    imgSrc: string;

    // Array of different segments in chart
    barChartData: ChartDataSets[] = [
        { data: [65, 59, 80, 81, 56, 55, 40, 56, 55, 40], label: 'Sum $/K' },
        { data: [28, 48, 40, 19, 86, 27, 90, 86, 27, 90], label: 'Sales', type:"line", lineTension:0, yAxisID: 'y-axis-sales'}
    ];
    
    //Labels shown on the x-axis
    barChartLabels: Label[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'May', 'June', 'July'];
    
    // Define chart options
    barChartOptions: ChartOptions = {
        responsive: true,
        scales: {
            xAxes: [{
                id: 'x-axis-time',
                scaleLabel: {
                    display: true,
                    labelString: 'Time'
                },
                ticks: {
                    autoSkip: true,
                    autoSkipPadding: 100,
                    padding: 10,
                },
                gridLines: {
                    drawTicks: false,
                }
            }],
            yAxes: [
              {
                id: 'y-axis-sum',
                position: 'left',
                scaleLabel: {
                    display: true,
                    labelString: 'Sum (in Thousands)',
                },
                ticks: {
                    max: Math.max(90), // add calculation here
                    min: 0
                },
              },
              {
                id: 'y-axis-sales',
                position: 'right',
                scaleLabel: {
                    display: true,
                    labelString: 'Number',
                },
                gridLines: {
                  display: false,
                },
                ticks: {
                    max: Math.max(100), // add calculation here
                    min: 0
                }
              }
            ],
        },
        legend: {
        display: true,
        labels: {
            // fontColor: 'rgb(255, 99, 132)'
            fontSize: 11,
        },
        position: "right"
        }
    };
    
    // Define colors of chart segments
    barChartColors: Color[] = [
    
        { // blue
        backgroundColor: 'rgba(45, 94, 185, 0.2)',
        borderColor: 'rgba(45, 94, 185, 0.5)',
        borderWidth: 1,
        },
        { // yellow/orange
        backgroundColor: 'rgba(0,0,0,0)',
        borderColor: 'orange',
        borderWidth: 2,
        }
    ];

    // Set true to show legends
    barChartLegend = true;
    
    // Define type of chart
    barChartType = 'bar';
    
    barChartPlugins = [];
    
    // events
    chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
        console.log(event, active);
    }
    
    chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
        console.log(event, active);
    }


    constructor(
        private salesService: SalesService,
        private alertService: AlertService,
    ) {
        this.startDate = new Date('2010-01-01');
        this.endDate = new Date('2022-01-01');
        this.scale = 0;
        this.saleStatistic = new SaleStatistic();
        this.isLoading = false;
        this.imgSrc = AppConstants.LOADING_GIF;
    }

    // Actions on initialization.
    ngOnInit(): void {
        this.loadStatistic();
    }

    // Load sales statistics from the server.
    loadStatistic(): void {

        this.isLoading = true;

        console.log('--->',this.scale, this.startDate, this.endDate);
        this.salesService.getStatistic(this.scale, this.startDate, this.endDate)
        .subscribe(
            data => {
                console.log('statisctic:',data);
                this.saleStatistic = data;
                this.isLoading = false;
                this.updateChart();
            },
            error => {
                this.saleStatistic = null;
                console.error(error);
                this.alertService.error(AppConstants.CONNECTION_ISSUES);
                this.isLoading = false;
            }
        );
    }

    updateChart(): void {
        this.barChartData = [
            { data: this.saleStatistic.salesSum, label: 'Sum $/K' },
            { data: this.saleStatistic.salesNumber, label: 'Sales', type:"line", lineTension:0, yAxisID: 'y-axis-sales' }
        ];
        this.barChartLabels = this.saleStatistic.salesDate;

        this.barChartOptions = {
            scales: {
                xAxes: [{
                    id: 'x-axis-time',
                    scaleLabel: {
                        display: true,
                        labelString: 'Time'
                    },
                    ticks: {
                        autoSkip: true,
                        autoSkipPadding: 100,
                        padding: 10,
                    },
                    gridLines: {
                        drawTicks: false,
                    }
                }],
                yAxes: [
                  {
                    id: 'y-axis-sum',
                    scaleLabel: {
                        display: true,
                        labelString: 'Sum (in Thousands)',
                    },
                    ticks: {
                        max: Math.ceil(Math.max(...this.saleStatistic.salesSum)*1.1),
                        min: 0
                    },
                  },
                  {
                    id: 'y-axis-sales',
                    position: 'right',
                    scaleLabel: {
                        display: true,
                        labelString: 'Number',
                    },
                    gridLines: {
                        display: false,
                      },
                    ticks: {
                        max: Math.ceil(Math.max(...this.saleStatistic.salesNumber)*1.1),
                        min: 0
                    }
                  }
                ],
            },
            legend: {
            display: true,
            labels: {
                fontSize: 11,
            },
            position: "right"
            }
        };
    }


    
}