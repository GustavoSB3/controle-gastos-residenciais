import { useEffect, useState } from 'react'
import { api } from '../services/api'
import type { Pessoa } from '../types'
import { Mensagem } from '../components/Mensagem'

export function PessoasPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [nome, setNome] = useState('')
  const [idade, setIdade] = useState('')
  const [mensagem, setMensagem] = useState('')

  const carregar = () => api.listarPessoas().then(setPessoas).catch(e => setMensagem(e.message))
  useEffect(() => {
    api.listarPessoas().then(setPessoas).catch(e => setMensagem(e.message))
  }, [])

  async function salvar(event: React.FormEvent) {
    event.preventDefault(); setMensagem('')
    try {
      await api.criarPessoa({ nome, idade: Number(idade) })
      setNome(''); setIdade(''); carregar()
    } catch (e) { setMensagem((e as Error).message) }
  }

  async function excluir(id: number) {
    if (!confirm('Excluir esta pessoa e todas as suas transações?')) return
    try { await api.excluirPessoa(id); carregar() } catch (e) { setMensagem((e as Error).message) }
  }

  return <section>
    <h2>Pessoas</h2>
    <form onSubmit={salvar} className="formulario">
      <label>Nome<input value={nome} onChange={e => setNome(e.target.value)} required minLength={2} /></label>
      <label>Idade<input type="number" min="0" max="130" value={idade} onChange={e => setIdade(e.target.value)} required /></label>
      <button type="submit">Cadastrar pessoa</button>
    </form>
    {mensagem && <Mensagem texto={mensagem} />}
    <div className="tabela-container"><table><thead><tr><th>Nome</th><th>Idade</th><th>Ação</th></tr></thead>
      <tbody>{pessoas.map(p => <tr key={p.id}><td>{p.nome}</td><td>{p.idade}</td><td><button className="perigo" onClick={() => excluir(p.id)}>Excluir</button></td></tr>)}</tbody>
    </table></div>
    {!pessoas.length && <p className="vazio">Nenhuma pessoa cadastrada.</p>}
  </section>
}
