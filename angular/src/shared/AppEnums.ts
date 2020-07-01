import { TenantAvailabilityState, ESearchParamOperatorTypes } from '@shared/service-proxies/service-proxies';

export class AppTenantAvailabilityState {
    static Available: number = TenantAvailabilityState._1;
    static InActive: number = TenantAvailabilityState._2;
    static NotFound: number = TenantAvailabilityState._3;
}

export class ESearchParamOperator {
    static Equal: number = ESearchParamOperatorTypes._1;
    static NotEqual: number = ESearchParamOperatorTypes._2;
    static Include: number = ESearchParamOperatorTypes._3;
    static SmallerThan: number = ESearchParamOperatorTypes._4;
    static GreaterThan: number = ESearchParamOperatorTypes._5;
    static SmallerThanOrEqual: number = ESearchParamOperatorTypes._6;
    static GreaterThanOrEqual: number = ESearchParamOperatorTypes._7;
}

export class EChartColors {
    static lines: string[] =
        [
            'rgba(41, 216, 255, 1)',
            'rgba(255, 36, 36, 1)',
            'rgba(71, 255, 96, 1)',
            'rgba(255, 116, 41, 1)',
            'rgba(255, 51, 177, 1)',
            'rgba(122, 231, 255, 1)',
            'rgba(255, 122, 122, 1)',
            'rgba(133, 255, 149, 1)',
            'rgba(255, 172, 128, 1)',
            'rgba(255, 143, 212, 1)',
            'rgba(209, 247, 255, 1)',
            'rgba(255, 214, 214, 1)',
            'rgba(204, 255, 211, 1)',
            'rgba(255, 209, 184, 1)',
            'rgba(255, 214, 239, 1)',
            'rgba(0, 129, 158, 1)',
            'rgba(153, 0, 0, 1)',
            'rgba(0, 158, 21, 1)',
            'rgba(199, 90, 0, 1)',
            'rgba(204, 0, 126, 1)'
        ];
    static areas: string[] =
        [
            'rgba(41, 216, 255, 0.6)',
            'rgba(255, 36, 36, 0.6)',
            'rgba(71, 255, 96, 0.6)',
            'rgba(255, 116, 41, 0.6)',
            'rgba(255, 51, 177, 0.6)',
            'rgba(122, 231, 255, 0.6)',
            'rgba(255, 122, 122, 0.6)',
            'rgba(133, 255, 149, 0.6)',
            'rgba(255, 172, 128, 0.6)',
            'rgba(255, 143, 212, 0.6)',
            'rgba(209, 247, 255, 0.6)',
            'rgba(255, 214, 214, 0.6)',
            'rgba(204, 255, 211, 0.6)',
            'rgba(255, 209, 184, 0.6)',
            'rgba(255, 214, 239, 0.6)',
            'rgba(0, 129, 158, 0.6)',
            'rgba(153, 0, 0, 0.6)',
            'rgba(0, 158, 21, 0.6)',
            'rgba(199, 90, 0, 0.6)',
            'rgba(204, 0, 126, 0.6)'
        ];
}
