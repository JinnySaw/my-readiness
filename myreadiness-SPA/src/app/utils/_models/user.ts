export interface User {
    id: string;
    userName: string;
    empID: number;
    email: string;
    knownAs: string;
    created: Date;
    lastActive: Date;
    roles?: string[];
}
