/* ==================================
   MiniEcommerce - Global JavaScript
   ================================== */

// Função para formatar valor em moeda brasileira
function formatarMoeda(valor) {
    return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
    }).format(valor);
}

// Função para formatar data
function formatarData(data) {
    return new Date(data).toLocaleDateString('pt-BR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    });
}

// Função para formatar CPF
function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}

// Função para remover formatação de CPF
function limparCPF(cpf) {
    return cpf.replace(/\D/g, '');
}

// Função para validar CPF
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

// Função para exibir mensagem de sucesso
function mostrarSucesso(mensagem, titulo = 'Sucesso!') {
    Swal.fire({
        icon: 'success',
        title: titulo,
        text: mensagem,
        timer: 2000,
        showConfirmButton: false
    });
}

// Função para exibir mensagem de erro
function mostrarErro(mensagem, titulo = 'Erro!') {
    Swal.fire({
        icon: 'error',
        title: titulo,
        text: mensagem
    });
}

// Função para exibir mensagem de confirmação
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

// Função para exibir loading
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

// Função para fechar loading
function fecharLoading() {
    Swal.close();
}

// Função para atualizar contador do carrinho
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

// Função para verificar se usuário está logado
function verificarLogin() {
    var usuario = sessionStorage.getItem('usuario');
    return usuario !== null;
}

// Função para obter dados do usuário da sessão
function obterUsuario() {
    var usuarioJson = sessionStorage.getItem('usuario');
    if (usuarioJson) {
        return JSON.parse(usuarioJson);
    }
    return null;
}

// Função para fazer logout
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

// Função para tratar erros de AJAX globalmente
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

// Inicialização quando o documento estiver pronto
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
    
    // Máscara de CPF em campos com classe 'cpf-mask'
    $('.cpf-mask').on('input', function() {
        var valor = $(this).val().replace(/\D/g, '');
        $(this).val(valor);
        
        if (valor.length === 11) {
            $(this).val(formatarCPF(valor));
        }
    });
    
    // Máscara de moeda em campos com classe 'money-mask'
    $('.money-mask').on('input', function() {
        var valor = $(this).val().replace(/\D/g, '');
        valor = (valor / 100).toFixed(2);
        $(this).val('R$ ' + valor.replace('.', ','));
    });
    
    // Confirmação antes de deletar
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

// Função para copiar texto para clipboard
function copiarParaClipboard(texto) {
    navigator.clipboard.writeText(texto).then(function() {
        mostrarSucesso('Copiado para a área de transferência!', '');
    }, function() {
        mostrarErro('Erro ao copiar para a área de transferência');
    });
}

// Função para rolar suavemente até um elemento
function rolarPara(elemento) {
    $('html, body').animate({
        scrollTop: $(elemento).offset().top - 100
    }, 500);
}

// Função para debounce (útil para busca em tempo real)
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

// Prevenir múltiplos cliques em botões
$('.btn').on('click', function() {
    var btn = $(this);
    if (btn.hasClass('btn-loading')) {
        return false;
    }
});

// Função para criar imagem placeholder
function imagemPlaceholder(texto = 'Sem Imagem') {
    return `data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='200' height='200'%3E%3Crect fill='%23ddd' width='200' height='200'/%3E%3Ctext fill='%23999' font-family='sans-serif' font-size='20' dy='10.5' font-weight='bold' x='50%25' y='50%25' text-anchor='middle'%3E${texto}%3C/text%3E%3C/svg%3E`;
}

// Tratar imagens quebradas
$('img').on('error', function() {
    $(this).attr('src', imagemPlaceholder('Imagem não encontrada'));
});

// Console personalizado
console.log('%c🛒 MiniEcommerce', 'color: #667eea; font-size: 20px; font-weight: bold;');
console.log('%cSistema carregado com sucesso!', 'color: #28a745; font-size: 14px;');
