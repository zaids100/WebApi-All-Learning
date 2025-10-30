import { mergeApplicationConfig, ApplicationConfig, provideZonelessChangeDetection } from '@angular/core';
import { provideServerRendering, withRoutes } from '@angular/ssr';
import { appConfig } from './app.config';
import { serverRoutes } from './app.routes.server';

// Use zoneless change detection for SSR so Zone.js is not required in server
const serverConfig: ApplicationConfig = {
  providers: [
    provideServerRendering(withRoutes(serverRoutes)),
    provideZonelessChangeDetection(),
  ]
};

export const config = mergeApplicationConfig(appConfig, serverConfig);
