import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ListeService } from '../services/liste-service';
import { Liste } from '../services/liste';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
// Cette class est le Component (ViewModel)
// Et permet d'aller chercher dans les models 
export class App {
  // inject => Injection de dépendance
  // ici par exemple 
  // singleton => toutes les demandes pour une instance de ListeService
  // n,'entraineront qu'un seule instanciation
  listeService = inject(ListeService);

  // dans les premières ms, liste sera undefined
  // liste est un Signal => Objet qui va avertir l;'UI si un changement de sa valeur survient
  liste=signal<Liste|undefined>(undefined);


  // Méthode exécutée au chargement de ce composant
  async ngOnInit() {
    // Après réponse du server, liste prendra une nouvelle valeur
    this.liste.set(await this.listeService.getListeAsync());
  }


}
