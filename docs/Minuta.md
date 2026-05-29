# Minuta de Relevamiento

## Contexto

El local Pastas Holores, ubicado en la ciudad de Rosario, decidió incorporar un sistema web para aumentar sus ventas y su reconocimiento en la ciudad. El sistema facilitará a los clientes la adquisición de todos sus productos y el pago de los mismos.

## Proceso Actual

Actualmente Pastas Holores recibe pedidos vía WhatsApp o presencialmente en su local. Los clientes pueden hacer un encargo previo de los productos que deseen adquirir. Una vez que el pedido se encuentra listo para su retiro, el local avisa al cliente por algún medio de comunicación pactado para que acuda al lugar a pagar y retirar su pedido. También disponen de stock para retirar inmediatamente en el local en caso de querer comprar cantidades menores.

## Proceso con el sistema de información deseado

Si un cliente desea adquirir un producto del local, debe ingresar al sitio web y seguir una serie de pasos:

1. Registrarse con sus datos personales en caso de no tener una cuenta, o iniciar sesión si ya posee una.
2. Una vez logueado, podrá acceder al catálogo completo de productos separado por categorías.
3. Seleccionar los productos que desee comprar y agregarlos al carrito, pudiendo elegir la cantidad de cada producto.
4. Acceder al carrito para ver el resumen del pedido, incluyendo el nombre, cantidad y precio de cada producto y el total a pagar.
5. Seleccionar el método de pago deseado (efectivo, tarjeta, Mercado Pago, etc).
6. Confirmar el pedido mediante el botón "Pagar", notificando al local para que prepare el pedido.

El cliente podrá modificar el carrito en cualquier momento, agregando o quitando productos y cantidades.

Por su parte, el administrador podrá realizar el alta, baja y modificación de los productos del catálogo, así como también gestionar los usuarios registrados en el sistema.

## Actores

- **Client**: usuario registrado que puede ver productos, gestionar su carrito y realizar pedidos.
- **Admin**: usuario con permisos completos para gestionar productos y clientes.

## Entidades principales

- **User**: clase base abstracta con los datos comunes de todos los usuarios (username, email, password, rol).
- **Admin**: hereda de User. Puede realizar ABM de productos.
- **Client**: hereda de User. Puede gestionar su carrito y realizar pedidos.
- **Product**: representa un producto del catálogo (nombre, precio, stock, categoría, disponibilidad).
- **Cart**: carrito de compras asociado a un cliente. Contiene una lista de productos, el precio total, el estado del pedido y el método de pago.