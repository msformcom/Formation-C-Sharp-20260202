import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ListeService } from '../services/liste-service';
import { Liste } from '../services/liste';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  // inject => Injection de dépendance
  // ici par exemple 
  // singleton => toutes les demandes pour une instance de ListeService
  // n,'entraineront qu'un seule instanciation
  listeService = inject(ListeService);

  // dans les premières ms, liste sera undefined
  liste?:Liste;


  // Méthode exécutée au chargement de ce composant
  async ngOnInit() {
    // Après réponse du server, liste sera connue
    this.liste=await this.listeService.getListe();
  }


}
