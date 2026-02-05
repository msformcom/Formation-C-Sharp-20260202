import { Injectable } from '@angular/core';
import { Liste } from './liste';

@Injectable({
  providedIn: 'root',
})
// Ce service permettra de gerer la liste
// Ce service est un Model (MVVM)
export class ListeService {
  // Obtenir la liste du server
  async getListeAsync(){
    // Envoyer la requête
    let reponseServer=await fetch("http://localhost:5295/liste");
    // Désérialiser le flux en json avec typage Liste
    let liste=await reponseServer.json() as Liste;
    return liste;
  }

  getList(){
     fetch("http://localhost:5295/liste").then(reponseServer=>{
        reponseServer.json().then(liste=>{
          return liste;
        })
     });
  }



}
