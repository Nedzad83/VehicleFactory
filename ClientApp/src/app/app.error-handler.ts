import * as Sentry from "@sentry/browser";
import { ToastrService } from 'ngx-toastr';
import { ErrorHandler, Inject, Injectable, Injector, NgZone, isDevMode } from "@angular/core"

Sentry.init({
    dsn: "https://7729e8eaa48e4b6b8944464c6ff2bf12@o393522.ingest.sentry.io/5242766"
  });

@Injectable() 
export class AppErrorHandler implements ErrorHandler
{
    constructor(
        private ngZone: NgZone,
        @Inject(Injector) private injector: Injector
    ) { }

    private get toastrService(): ToastrService {
        return this.injector.get(ToastrService);
      }

    handleError(error: any): void {
        const eventId = Sentry.captureException(error.originalError || error);
        Sentry.showReportDialog({ eventId });
        this.ngZone.run(() => { 
            console.log("ERROR");

            this.toastrService.error('An unexpected error happened.', 'Error', {
                timeOut: 3000
            });
        });
    } 
}