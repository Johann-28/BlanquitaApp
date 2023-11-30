export class ProductDTO {
idProducto?: number;
idTipoProducto?: number;
descripcion?: string;
precio?: number; 
}

export class ProductTypeDTO {
    idTipoProducto?: number;
    clave?: string;
    descripcion?: string;
}