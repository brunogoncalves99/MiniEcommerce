function formatarMoeda(valor) {
    return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
    }).format(valor);
}

function formatarData(data) {
    return new Date(data).toLocaleDateString('pt-BR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    });
}

function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}

function limparCPF(cpf) {
    return cpf.replace(/\D/g, '');
}

function validarCPF(cpf) {
    cpf = limparCPF(cpf);
    
    if (cpf.length !== 11) return false;
    
    // Verifica se todos os dígitos são iguais
    if (/^(\d)\1+$/.test(cpf)) return false;
    
    // Validação do primeiro dígito verificador
    let soma = 0;
    for (let i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }
    let resto = 11 - (soma % 11);
    let digito1 = resto >= 10 ? 0 : resto;
    
    if (parseInt(cpf.charAt(9)) !== digito1) return false;
    
    // Validação do segundo dígito verificador
    soma = 0;
    for (let i = 0; i < 10; i++) {
        soma += parseInt(cpf.charAt(i)) * (11 - i);
    }
    resto = 11 - (soma % 11);
    let digito2 = resto >= 10 ? 0 : resto;
    
    return parseInt(cpf.charAt(10)) === digito2;
}

function mostrarSucesso(mensagem, titulo = 'Sucesso!') {
    Swal.fire({
        icon: 'success',
        title: titulo,
        text: mensagem,
        timer: 2000,
        showConfirmButton: false
    });
}

function mostrarErro(mensagem, titulo = 'Erro!') {
    Swal.fire({
        icon: 'error',
        title: titulo,
        text: mensagem
    });
}

function confirmar(mensagem, titulo = 'Confirmar') {
    return Swal.fire({
        title: titulo,
        text: mensagem,
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não',
        confirmButtonColor: '#667eea',
        cancelButtonColor: '#6c757d'
    });
}

function mostrarLoading(mensagem = 'Carregando...') {
    Swal.fire({
        title: mensagem,
        allowOutsideClick: false,
        allowEscapeKey: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });
}

function fecharLoading() {
    Swal.close();
}

function atualizarContadorCarrinho() {
    $.ajax({
        url: '/Carrinho/Obter',
        type: 'GET',
        success: function(response) {
            if (response.sucesso && response.dados && response.dados.itens) {
                var total = response.dados.itens.length;
                $('#carrinho-count').text(total);
                
                if (total > 0) {
                    $('#carrinho-count').show();
                } else {
                    $('#carrinho-count').hide();
                }
            } else {
                $('#carrinho-count').text('0').hide();
            }
        },
        error: function() {
            $('#carrinho-count').text('0').hide();
        }
    });
}

function verificarLogin() {
    var usuario = sessionStorage.getItem('usuario');
    return usuario !== null;
}

function obterUsuario() {
    var usuarioJson = sessionStorage.getItem('usuario');
    if (usuarioJson) {
        return JSON.parse(usuarioJson);
    }
    return null;
}

function logout() {
    confirmar('Deseja realmente sair?', 'Logout').then((result) => {
        if (result.isConfirmed) {
            mostrarLoading('Saindo...');
            
            $.ajax({
                url: '/Autenticacao/Logout',
                type: 'POST',
                success: function() {
                    sessionStorage.clear();
                    window.location.href = '/Autenticacao/Login';
                },
                error: function() {
                    fecharLoading();
                    mostrarErro('Erro ao fazer logout');
                }
            });
        }
    });
}

$(document).ajaxError(function(event, jqxhr, settings, thrownError) {
    if (jqxhr.status === 401) {
        mostrarErro('Sessão expirada. Faça login novamente.');
        setTimeout(function() {
            window.location.href = '/Autenticacao/Login';
        }, 2000);
    } else if (jqxhr.status === 403) {
        mostrarErro('Você não tem permissão para realizar esta ação.');
    } else if (jqxhr.status === 500) {
        mostrarErro('Erro interno do servidor. Tente novamente mais tarde.');
    }
});

$(document).ready(function() {
    // Adicionar classe fade-in aos cards
    $('.card').addClass('fade-in');
    
    // Tooltip do Bootstrap
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
    
    // Atualizar contador do carrinho ao carregar a página
    if (window.location.pathname !== '/Autenticacao/Login') {
        atualizarContadorCarrinho();
    }
    
    $('.cpf-mask').on('input', function() {
        var valor = $(this).val().replace(/\D/g, '');
        $(this).val(valor);
        
        if (valor.length === 11) {
            $(this).val(formatarCPF(valor));
        }
    });
    
    $('.money-mask').on('input', function() {
        var valor = $(this).val().replace(/\D/g, '');
        valor = (valor / 100).toFixed(2);
        $(this).val('R$ ' + valor.replace('.', ','));
    });
    
    $('.btn-delete').on('click', function(e) {
        e.preventDefault();
        var url = $(this).attr('href') || $(this).data('url');
        
        confirmar('Deseja realmente excluir este item?', 'Confirmar Exclusão').then((result) => {
            if (result.isConfirmed) {
                window.location.href = url;
            }
        });
    });
    
    // Auto-hide de alerts após 5 segundos
    $('.alert').each(function() {
        var alert = $(this);
        setTimeout(function() {
            alert.fadeOut('slow', function() {
                $(this).remove();
            });
        }, 5000);
    });
});

function copiarParaClipboard(texto) {
    navigator.clipboard.writeText(texto).then(function() {
        mostrarSucesso('Copiado para a área de transferência!', '');
    }, function() {
        mostrarErro('Erro ao copiar para a área de transferência');
    });
}

function rolarPara(elemento) {
    $('html, body').animate({
        scrollTop: $(elemento).offset().top - 100
    }, 500);
}

function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

$('.btn').on('click', function() {
    var btn = $(this);
    if (btn.hasClass('btn-loading')) {
        return false;
    }
});

function imagemPlaceholder(texto = 'Sem Imagem') {
    return `data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='200' height='200'%3E%3Crect fill='%23ddd' width='200' height='200'/%3E%3Ctext fill='%23999' font-family='sans-serif' font-size='20' dy='10.5' font-weight='bold' x='50%25' y='50%25' text-anchor='middle'%3E${texto}%3C/text%3E%3C/svg%3E`;
}

$('img').on('error', function() {
    $(this).attr('src', imagemPlaceholder('Imagem não encontrada'));
});
