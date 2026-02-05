import { Injectable } from '@angular/core';
import { Liste } from './liste';

@Injectable({
  providedIn: 'root',
})
// Ce service permettra de gerer la liste
export class ListeService {
  // Obtenir la liste du server
  async getListe(){
    // Envoyer la requête
    let reponseServer=await fetch("http://localhost:5295/liste");
    // Désérialiser le flux en json avec typage Liste
    let liste=await reponseServer.json() as Liste;
    return liste;
  }
}
