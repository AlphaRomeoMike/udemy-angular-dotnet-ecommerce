export interface IGenericResponse<T> {
    data: T;
    message: string;
    success: boolean;
}