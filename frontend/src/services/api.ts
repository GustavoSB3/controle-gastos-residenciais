import type { Pessoa, Totais, Transacao, TipoTransacao } from '../types'

const API_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5000/api'

async function request<T>(path: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${API_URL}${path}`, {
    headers: { 'Content-Type': 'application/json' }, ...options,
  })
  if (!response.ok) {
    const body = await response.json().catch(() => null)
    throw new Error(body?.mensagem ?? body?.title ?? 'Não foi possível concluir a operação.')
  }
  return response.status === 204 ? (undefined as T) : response.json()
}

export const api = {
  listarPessoas: () => request<Pessoa[]>('/pessoas'),
  criarPessoa: (dados: { nome: string; idade: number }) => request<Pessoa>('/pessoas', { method: 'POST', body: JSON.stringify(dados) }),
  excluirPessoa: (id: number) => request<void>(`/pessoas/${id}`, { method: 'DELETE' }),
  listarTransacoes: () => request<Transacao[]>('/transacoes'),
  criarTransacao: (dados: { descricao: string; valor: number; tipo: TipoTransacao; pessoaId: number }) =>
    request<Transacao>('/transacoes', { method: 'POST', body: JSON.stringify(dados) }),
  obterTotais: () => request<Totais>('/totais'),
}
