window.onload = function () {

    //engrenagem
    var gear = document.querySelector('#gear')
    gear.addEventListener('mouseenter', () => { gear.src = 'http://localhost:5000/img/icons/gear-focus.png' })
    gear.addEventListener('mouseout', () => { gear.src = 'http://localhost:5000/img/icons/gear.png' })

    //carrinho
    var cart = document.querySelector('#cart')
    cart.addEventListener('mouseenter', () => { cart.src = 'http://localhost:5000/img/icons/cart-focus.png' })
    cart.addEventListener('mouseout', () => { cart.src = 'http://localhost:5000/img/icons/cart.png'
    })
}