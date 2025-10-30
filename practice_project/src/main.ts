import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { provideZonelessChangeDetection } from '@angular/core';

bootstrapApplication(App, {
  ...appConfig,
  providers: [
    ...(appConfig.providers || []),
    provideZonelessChangeDetection(), // ✅ new correct API name
  ],
})
.catch(err => console.error(err));
