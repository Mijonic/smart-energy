
import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public innerWidth: any;
  public containerLen!:number;
  public innerHeight: any;
  public containerHeight!:number;

  
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.innerHeight;
    if(this.innerWidth <= 990)
    {
      this.containerLen = this.innerWidth-10;
    }
    else
    {
      this.containerLen = this.innerWidth - 210; 
    }
    this.containerHeight = this.innerHeight - 76;
  }

  constructor(private _router: Router) { } 

  ngAfterViewInit(): void {
    
  }

  ngOnInit(): void {
    window.dispatchEvent(new Event('resize'));
  }

  get isHomePage():boolean{
    return this._router.url ==="/";
  }
   

 


}
