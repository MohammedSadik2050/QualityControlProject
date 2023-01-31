import {
    Component,
    OnInit,
    ViewEncapsulation,
    Injector,
    Renderer2
} from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
    templateUrl: './account.component.html',
    styleUrls: ['./account.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AccountComponent extends AppComponentBase implements OnInit {
    rtl: string;
    constructor(injector: Injector, private renderer: Renderer2) {
        super(injector);
    }

    showTenantChange(): boolean {
        abp.multiTenancy.isEnabled = false;
        return abp.multiTenancy.isEnabled;
    }

    ngOnInit(): void {
        this.renderer.addClass(document.body, 'login-page');
        var lang = abp.utils.getCookieValue('Abp.Localization.CultureName');
        console.log('lang', lang);
        if (lang !== 'en') {
            this.rtl = 'rtl';
        }
    }
}
