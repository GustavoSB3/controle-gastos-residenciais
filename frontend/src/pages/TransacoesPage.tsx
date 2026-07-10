import { useEffect, useState } from 'react'
import { Mensagem } from '../components/Mensagem'
import { api } from '../services/api'
import type { Pessoa, TipoTransacao, Transacao } from '../types'

export function TransacoesPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [transacoes, setTransacoes] = useState<Transacao[]>([])
  const [descricao, setDescricao] = useState('')
  const [valor, setValor] = useState('')
  const [tipo, setTipo] = useState<TipoTransacao>(2)
  const [pessoaId, setPessoaId] = useState('')
  const [mensagem, setMensagem] = useState('')

  const atualizarListas = ([p, t]: [Pessoa[], Transacao[]]) => {
    setPessoas(p); setTransacoes(t)
    setPessoaId(atual => atual || (p[0] ? String(p[0].id) : ''))
  }
  const carregar = () => Promise.all([api.listarPessoas(), api.listarTransacoes()])
    .then(atualizarListas)
    .catch(e => setMensagem(e.message))
  useEffect(() => {
    Promise.all([api.listarPessoas(), api.listarTransacoes()])
      .then(atualizarListas).catch(e => setMensagem(e.message))
  }, [])

  async function salvar(event: React.FormEvent) {
    event.preventDefault(); setMensagem('')
    try {
      await api.criarTransacao({ descricao, valor: Number(valor), tipo, pessoaId: Number(pessoaId) })
      setDescricao(''); setValor(''); carregar()
    } catch (e) { setMensagem((e as Error).message) }
  }

  return <section><h2>Transações</h2>
    <form onSubmit={salvar} className="formulario">
      <label>Descrição<input value={descricao} onChange={e => setDescricao(e.target.value)} required minLength={2} /></label>
      <label>Valor<input type="number" min="0.01" step="0.01" value={valor} onChange={e => setValor(e.target.value)} required /></label>
      <label>Tipo<select value={tipo} onChange={e => setTipo(Number(e.target.value) as TipoTransacao)}><option value={1}>Receita</option><option value={2}>Despesa</option></select></label>
      <label>Pessoa<select value={pessoaId} onChange={e => setPessoaId(e.target.value)} required><option value="" disabled>Selecione</option>{pessoas.map(p => <option key={p.id} value={p.id}>{p.nome} ({p.idade} anos)</option>)}</select></label>
      <button type="submit" disabled={!pessoas.length}>Cadastrar transação</button>
    </form>
    {mensagem && <Mensagem texto={mensagem} />}
    {!pessoas.length && <Mensagem texto="Cadastre uma pessoa antes de adicionar transações." />}
    <div className="tabela-container"><table><thead><tr><th>Descrição</th><th>Pessoa</th><th>Tipo</th><th>Valor</th></tr></thead><tbody>
      {transacoes.map(t => <tr key={t.id}><td>{t.descricao}</td><td>{t.pessoaNome}</td><td><span className={t.tipo === 1 ? 'receita' : 'despesa'}>{t.tipo === 1 ? 'Receita' : 'Despesa'}</span></td><td>{t.valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</td></tr>)}
    </tbody></table></div>{!transacoes.length && <p className="vazio">Nenhuma transação cadastrada.</p>}
  </section>
}
