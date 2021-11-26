window.onload = function () {

    //engrenagem
    var gear = document.querySelector('#gear')
    gear.addEventListener('mouseenter', () => { gear.src = 'https://raw.githubusercontent.com/Eric-Pessoa/Made-4-Pet/main/Made-4-Pet/Made-4-Pet/wwwroot/img/icons/gear-focus.png' })
    gear.addEventListener('mouseout', () => { gear.src = 'https://raw.githubusercontent.com/Eric-Pessoa/Made-4-Pet/main/Made-4-Pet/Made-4-Pet/wwwroot/img/icons/gear.png' })

    //carrinho
    var cart = document.querySelector('#cart')
    cart.addEventListener('mouseenter', () => { cart.src = 'https://raw.githubusercontent.com/Eric-Pessoa/Made-4-Pet/main/Made-4-Pet/Made-4-Pet/wwwroot/img/icons/cart-focus.png' })
    cart.addEventListener('mouseout', () => { cart.src = 'https://raw.githubusercontent.com/Eric-Pessoa/Made-4-Pet/main/Made-4-Pet/Made-4-Pet/wwwroot/img/icons/cart.png'
    })
}