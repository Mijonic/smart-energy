import { WorkRequestService } from './../services/work-request.service';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { WorkRequestStatistics } from 'app/shared/models/work-request-statistics.model';
import { ChartComponent } from 'ng-apexcharts';
import { IncidentsStatistics } from 'app/shared/models/incident-statistics.mode';
import { IncidentService } from 'app/services/incident.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  @ViewChild("areachart") areachart!: ChartComponent;
  @ViewChild("piechart") piechart!: ChartComponent;
  public chartOptionsArea: any;

  public chartOptionsPie: any;

  public shouldReset: boolean = true;
  isLoadingWorkReq:boolean = true;
  isLoadingIncident:boolean = true;

  workReqStats:WorkRequestStatistics =  new WorkRequestStatistics();
  incidentStats:IncidentsStatistics = new IncidentsStatistics();

  public generateData(baseval: any, count: any, yrange: any) {
    var i = 0;
    var series = [];
    while (i < count) {
      var x = Math.floor(Math.random() * (750 - 1 + 1)) + 1;
      var y =
        Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;
      var z = Math.floor(Math.random() * (75 - 15 + 1)) + 15;
  
      series.push([x, y, z]);
      baseval += 86400000;
      i++;
    }
    return series;
  }


  constructor(private changeDetectorRef: ChangeDetectorRef, private workReqService:WorkRequestService, private incidentService: IncidentService) {

   }

   loadWorkRequestStats()
   {
     this.isLoadingWorkReq = true;
     let userId:number = +JSON.parse(localStorage.getItem("user")!).id;

     this.workReqService.getWorkRequestStatistics(userId).subscribe(
       data=>{
         this.workReqStats = data;
         this.isLoadingWorkReq = false;
         this.initAreaChart("100%");
       }
     )
   }

   loadIncidentsStats()
   {
     this.isLoadingIncident = true;
     let userId:number = +JSON.parse(localStorage.getItem("user")!).id;

     this.incidentService.getIncidentsStatistics(userId).subscribe(
       data=>{
         this.incidentStats = data;
         this.isLoadingIncident = false;
         this.initAreaChart("100%");
       }
     )
   }

  

    resetCharts() {
        this.initAreaChart("100%");
        this.initPieChar("100%");

     
    }

    initAreaChart(width: any) {
      this.chartOptionsPie = {
        series: [55, this.workReqStats.total, this.incidentStats.total],
        labels:  ['WP', 'WR', 'INC'],
        title: {
          text: 'DOCUMENTS',
          align: 'center',
          
          offsetX: 0,
          offsetY: 0,
          floating: false,
          style: {
            fontSize:  '16px',
            fontWeight:  'bold',
            fontFamily:  'Arial',
            color:  'white'
          },
        },  
       
        chart: {
          width: width,
          type: "donut",
          foreColor: 'white',
          height:350
        },
        dataLabels: {
          enabled: true,
        },
        fill: {
          type: "gradient"
        },
    
        
        
        legend: {
          formatter: function(val: any, opts: any) {
            return val + " - " + opts.w.globals.series[opts.seriesIndex];
          }
          
        
        },
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200
              },
              legend: {
                position: "bottom"
              }
            }
          }
        ]
      };
    }

    initPieChar(width: any) {
      this.chartOptionsArea  = {
        series: [
          {
            name: "planed",
            data: [31, 40, 28, 51, 42, 109, 100]
          },
          {
            name: "unplaned",
            data: [11, 32, 45, 32, 34, 52, 41]
          }
        ],
        title: {
          text: 'INCIDENTS',
          align: 'center',
          
          offsetX: 0,
          offsetY: 0,
          floating: false,
          style: {
            fontSize:  '16px',
            fontWeight:  'bold',
            fontFamily:  'Arial',
            color:  'white'
          },
        },  
        chart: {
          height: 350,
          type: "area",
          width: width
        },
        dataLabels: {
          enabled: true
        },
        stroke: {
          curve: "smooth"
        },
        
        xaxis: {
          type: "datetime",
          categories: [
            "2018-09-19T00:00:00.000Z",
            "2018-09-19T01:30:00.000Z",
            "2018-09-19T02:30:00.000Z",
            "2018-09-19T03:30:00.000Z",
            "2018-09-19T04:30:00.000Z",
            "2018-09-19T05:30:00.000Z",
            "2018-09-19T06:30:00.000Z"
          ]
        },
        tooltip: {
          x: {
            format: "dd/MM/yy HH:mm"
          }
        }
      };
  
    }


  ngOnInit() {

    window.dispatchEvent(new Event('resize'));
    this.loadWorkRequestStats();
    this.loadIncidentsStats();
    this.initAreaChart(700);
    this.initPieChar(400);

    

    if(this.shouldReset)
    {
      setTimeout(() => { this.resetCharts() }, 150);
      //setTimeout(() => { window.dispatchEvent(new Event('resize')); }, 150);
      this.shouldReset = false;

    }

  

  }

 

}
