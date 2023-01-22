# Trabajo Practico Base de Datos II

## Objetivo

Se pide construir una aplicación para gestionar la creación de pedidos registrando: 
Los datos de los usuarios que realizaran los pedidos.
El carrito de compras de estos.
La conversión de esos carritos de compras en pedidos.
La finalización de esos pedidos y su conversión a facturas. 
Los pagos de las facturas.
La imputación de los pagos a la factura de los usuarios. 

### Requerimientos

- Guardar y recuperar la sesión del usuario conectado junto con su actividad. 

- Gestionar los productos seleccionados junto con la cantidad de este en un carrito de compras (agregar, eliminar, cambiar cantidad).

- Guardar y recuperar o volver a estados anteriores en las acciones realizadas sobre un carrito de compras activo.

- Convertir el contenido del carrito de compras en un pedido (indicando el contenido de este, los datos del cliente (nombre, apellido, dirección, condición ante el IVA, etc.) el importe de los artículos y los impuestos si correspondiera según su condición.

- Facturar el pedido y registrar en pago indicando la forma del pago (efectivo, tarjeta, cta. cte., etc.).

- Llevar el control de todas las operaciones de facturación y pagos, indicando el operador de esta.

- Llevar un catálogo de los productos con su descripción, fotos, comentarios, videos explicativos o publicitarios y toda información que se considere de interés a fin de hacer más atractiva la experiencia de compra. 

- Llevar un registro de todas las actividades realizadas sobre el contenido del catálogo de los productos, indicando el valor anterior, el nuevo valor y el operador. 

## Arquitectura

![E-Commerce](https://user-images.githubusercontent.com/58712215/199310179-8a30de28-5bd5-4a0c-bd30-b1566b529da4.jpeg)


