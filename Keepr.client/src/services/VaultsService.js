import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  async createKV(kvBody2) {
    const kv = { keepId: AppState.activeKeep.id, vaultId: kvBody2 }
    logger.log(kv)
    const res = await api.post('/api/vaultkeeps', kv)
    logger.log('createkv', res.data)
    AppState.vaultkeeps = res.data
  }

  async createVault(vaultData) {
    logger.log(vaultData)
    const res = await api.post('/api/vaults', vaultData)
    logger.log(res.data)
    AppState.myvaults = [...AppState.myvaults, res.data]
  }
}

export const vaultsService = new VaultsService()