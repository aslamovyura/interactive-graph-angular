import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import 'hammerjs'
import 'chartjs-plugin-zoom'

const platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);