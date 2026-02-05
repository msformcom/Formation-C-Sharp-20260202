import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    // Automatiser les maj de l'UI en fonction des changements dans les component
    //provideZoneChangeDetection(),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes)
  ]
};
