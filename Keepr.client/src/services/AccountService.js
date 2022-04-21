import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyVaults(id) {
    id = AppState.account.id
    const res = await api.get('/account/vaults')
    logger.log('getmyvaults', res.data)
    AppState.myvaults = res.data
  }
}

export const accountService = new AccountService()
