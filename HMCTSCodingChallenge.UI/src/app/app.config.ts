import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';

export const appConfig: ApplicationConfig = {
providers: [
    provideHttpClient(),
    importProvidersFrom(FormsModule),
    provideAnimations(),
    providePrimeNG({ theme: { preset: Aura } })
]
};
