export interface User {
    id: number;
    userName: string;
    created: DataCue;
    lastActive: Date;
    roles?: string[];
}
