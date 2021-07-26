
import { Component, OnInit } from '@angular/core';
import { Main } from 'ng-particles';
import { ISourceOptions } from 'tsparticles';





@Component({
  selector: 'app-particles',
  templateUrl: './particles.component.html',
  styleUrls: ['./particles.component.css']
})
export class ParticlesComponent implements OnInit {

  id = "tsparticles";
    
  /* Starting from 1.19.0 you can use a remote url (AJAX request) to a JSON with the configuration */
  
  particlesOptions: ISourceOptions = {

      background: {
        color: {
            value: "#666666"
          
        }
      },
     
      fpsLimit: 60,
      interactivity: {
          
          detectsOn: "canvas",
          events: {
              onClick: {
                  enable: true,
                  mode: "push"
              },
              onHover: {
                  enable: true,
                  mode: "grab"
              },
              resize: true
          },
          modes: {
              grab:{
                distance: 100,
                line_linked:{
                  opacity: 1
                }
              },
              bubble: {
                  distance: 400,
                  duration: 2,
                  opacity: 0.8,
                  size: 40
              },
              push: {
                  quantity: 4
              },
              repulse: {
                  distance: 200,
                  duration: 0.4
              }
          }
      },

      backgroundMode: {
        enable: true,
        zIndex: -1
    } ,
      particles: {
        
          color: {
              value: "#ffffff"
          },
          links: {
              color: "#ffffff",
              distance: 150,
              enable: true,
              opacity: 0.5,
              width: 1
          },
          collisions: {
              enable: true
          },
          move: {
              direction: "none",
              enable: true,
              outMode: "bounce",
              random: false,
              speed: 1,
              straight: false
          },
          number: {
              density: {
                  enable: true,
                  value_area: 800
              },
              value: 30
          },
          opacity: {
              value: 1
          },
          shape: {
              type: "image",
              image: {
                src: '../assets/Images/1.png',
                width: 100,
                height: 100
              }
              
          },
          size: {
              random: true,
              value: 25
          }
      },

      
      
      detectRetina: true,
      
  };

  particlesLoaded(container: any): void {
    
  }
  
  particlesInit(main: Main): void {
      
      // Starting from 1.19.0 you can add custom presets or shape here, using the current tsParticles instance (main)
  }


 
  

  constructor() { }

  ngOnInit(): void {

   

  }


  

 
 



}
