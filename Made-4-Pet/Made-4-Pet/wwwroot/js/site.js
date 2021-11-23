window.onload = function () {

    //engrenagem
    var gear = document.querySelector('#gear')
    gear.addEventListener('mouseenter', () => { gear.src = '../img/icons/gear-focus.png' })
    gear.addEventListener('mouseout', () => { gear.src = '../img/icons/gear.png' })

    //carrinho
    var cart = document.querySelector('#cart')
    cart.addEventListener('mouseenter', () => { cart.src = '../img/icons/cart-focus.png' })
    cart.addEventListener('mouseout', () => { cart.src = '../img/icons/cart.png'
    })
}