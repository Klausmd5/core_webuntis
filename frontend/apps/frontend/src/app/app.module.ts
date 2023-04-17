import {APP_INITIALIZER, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {ApiModule, Configuration} from './backend';
import {HttpClientModule} from '@angular/common/http';
import {ConfigurationService} from "./core/configuration.service";

export function initConfig(configService: ConfigurationService): () => Promise<void> {
  return async () => {
    await configService.init();
  };
}

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, ApiModule, HttpClientModule],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initConfig,
      deps: [ConfigurationService],
      multi: true
    },
    {
      provide: Configuration,
      useFactory: (configService: ConfigurationService) => configService.getConfig(),
      deps: [ConfigurationService],
      multi: false
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
