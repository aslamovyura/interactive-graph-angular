import { Component, OnInit, Input } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Label, Color } from 'ng2-charts';
import { SalesService, AlertService } from '../_services';
import { SaleStatistic } from '../_models';
import { AppConstants } from '../_constants';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'home-app',
    templateUrl: './home.component.html',
    styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit {

    @Input() startDate: Date;
    @Input() endDate: Date;
    @Input() scale: number;
    saleStatistic: SaleStatistic;
    isLoading: boolean;
    imgSrc: string;
    controlsForm: FormGroup;

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
                },
            }],
            yAxes: [
              {
                id: 'y-axis-sum',
                position: 'left',
                scaleLabel: {
                    display: true,
                    labelString: 'Sum (in Thousands)',
                },
                // ticks: {
                //     max: Math.max(90), // add calculation here
                //     min: 0
                // },
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
                // ticks: {
                //     max: Math.max(100), // add calculation here
                //     min: 0
                // }
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
        },
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
        // console.log(event, active);
    }
    
    chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
        // console.log(event, active);
    }


    constructor(
        private salesService: SalesService,
        private alertService: AlertService,
        private datePipe: DatePipe,
        private formBuilder: FormBuilder,
    ) {
        this.startDate = new Date('2010-01-01');
        this.endDate = new Date('2022-01-01');;
        this.scale = 1;
        this.saleStatistic = new SaleStatistic();
        this.isLoading = false;
        this.imgSrc = AppConstants.LOADING_GIF;

        this.controlsForm = new FormGroup({
            scale: new FormControl(),
            startDate: new FormControl(),
            endDate: new FormControl()
         });
    }

    // Actions on initialization.
    ngOnInit(): void {
        this.loadStatistics();
    }

    // Load sales statistics from the server.
    loadStatistics(): void {

        this.isLoading = true;
        this.salesService.getStatistic(this.scale, this.startDate, this.endDate)
        .subscribe(
            data => {
                if (data.salesDate.length == 0){
                    this.alertService.error(AppConstants.INCORRECT_PARAMETERS);
                    this.isLoading = false;
                }
                else {
                    this.saleStatistic = data;
                    this.fillEditProfileForm();
                    this.isLoading = false;
                    this.updateChart();
                }
            },
            error => {
                this.saleStatistic = null;
                this.fillEditProfileForm();
                console.error(error);
                this.alertService.error(AppConstants.CONNECTION_ISSUES);
                this.isLoading = false;
            }
        );
    }

    updateChart(): void {
        this.barChartData = [
            // barThickness: 16, barPercentage: 100,
            { data: this.saleStatistic.salesSum, label: 'Sum $/K' },
            { data: this.saleStatistic.salesNumber, label: 'Sales', type:"line", lineTension:0, yAxisID: 'y-axis-sales' }
        ];
        this.barChartLabels = this.convertDateToString(this.saleStatistic.salesDate);

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
                        maxTicksLimit: 5,
                        maxRotation: 0,
                        padding: 10,
                    },
                    gridLines: {
                        drawTicks: false,
                    },
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
            },
            tooltips: {
                mode: 'nearest',
                position: 'nearest',
                callbacks: {
                    title: function(tooltipItem, data) {
                        return null;
                    },
                    label: function(tooltipItem, data) {

                        if(tooltipItem['datasetIndex'] == 0){
                            return "Sales on "
                            + data['labels'][tooltipItem['index']].toString() + ":  "
                            + data['datasets'][0]['data'][tooltipItem['index']].toString() + '$';
                        } 
                        else {
                            return "Sales on "
                            + data['labels'][tooltipItem['index']].toString() + ":  "
                            + data['datasets'][1]['data'][tooltipItem['index']].toString();
                        }  
                    },
                },
                backgroundColor: 'rgba(248, 136, 230, 0.2)',
                bodyFontColor: 'black',
                bodyFontSize: 12,
                displayColors: false,
              },
        };
    }

    private convertDateToString(dates: Date[]) : string[] {

        let strings = new Array<string>();
        for (let date of dates){
            strings.push(this.datePipe.transform(date, 'yyyy/MM/dd').toString());
        }
        return strings;
    }

    // Getter for easy access to controls form fields.
    get f() { return this.controlsForm.controls; }

    // Fill contols with initial data.
    fillEditProfileForm() {
        this.controlsForm = this.formBuilder.group({
            scale:      [ this.scale, [Validators.required]],
            startDate:  [ this.datePipe.transform(this.startDate, 'yyyy-MM-dd'), [Validators.required]],
            endDate:    [ this.datePipe.transform(this.endDate, 'yyyy-MM-dd'), [Validators.required]],
        });
    }
    
    inputChange(){
        console.log('Parameters are changed!');
        this.loadStatistics();
    }
}
