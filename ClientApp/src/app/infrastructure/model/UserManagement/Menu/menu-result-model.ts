export interface MenuResultModel{
    id: number;
    title: string;
    class?: string;
    faClass?: string;
    routeUrl: string;
    rank: number;
    children?: MenuResultModel[];
}