using System;
using System.Collections.Generic;
using System.Linq;
using EP.CursoMvc.Domain.Validations.Clientes;

namespace EP.CursoMvc.Domain.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }
        public bool Excluido { get; private set; }

        public virtual ICollection<Endereco> Enderecos { get; private set; }

        public Cliente(string nome, string email, string cpf, DateTime dataNascimento, bool ativo)
        {
            Nome = nome;
            Email = email;
            CPF = cpf;
            DataNascimento = dataNascimento;
            DataCadastro = DateTime.Now;
            Ativo = ativo;
            Enderecos = new List<Endereco>();
        }

        public Cliente()
        {
            Enderecos = new List<Endereco>();
        }

        public void TrocarEmail(string email)
        {
            // validar se o email eh correto antes de setar
            Email = email;
        }

        public void DefinirComoExcluido()
        {
            Ativo = false;
            Excluido = true;
        }

        public void DefinirComoAtivo()
        {
            Ativo = true;
            Excluido = false;
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            if(endereco.EhValido())
                Enderecos.Add(endereco);
        }

        public override bool EhValido()
        {
            ValidationResult = new ClienteEstaConsistenteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}