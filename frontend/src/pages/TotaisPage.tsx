import { useEffect, useState } from 'react'
import { Mensagem } from '../components/Mensagem'
import { api } from '../services/api'
import type { Totais } from '../types'

const moeda = (valor: number) => valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })

export function TotaisPage() {
  const [totais, setTotais] = useState<Totais | null>(null)
  const [erro, setErro] = useState('')
  useEffect(() => { api.obterTotais().then(setTotais).catch(e => setErro(e.message)) }, [])
  if (erro) return <Mensagem texto={erro} />
  if (!totais) return <p>Carregando...</p>
  return <section><h2>Totais</h2><div className="tabela-container"><table><thead><tr><th>Pessoa</th><th>Receitas</th><th>Despesas</th><th>Saldo</th></tr></thead><tbody>
    {totais.pessoas.map(p => <tr key={p.pessoaId}><td>{p.nome}</td><td className="receita">{moeda(p.totalReceitas)}</td><td className="despesa">{moeda(p.totalDespesas)}</td><td>{moeda(p.saldo)}</td></tr>)}
  </tbody><tfoot><tr><th>Total geral</th><th>{moeda(totais.totalGeralReceitas)}</th><th>{moeda(totais.totalGeralDespesas)}</th><th>{moeda(totais.saldoLiquidoGeral)}</th></tr></tfoot></table></div>
  {!totais.pessoas.length && <p className="vazio">Nenhuma pessoa cadastrada.</p>}</section>
}
