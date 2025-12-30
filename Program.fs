// For more information see https://aka.ms/fsharp-console-apps
type Locked = Locked
type Unlocked = Unlocked

type PasswordManager<'state> =
    private { password: string
              passwords: string list }
    
module PasswordManager =
    
    let create : PasswordManager<Locked> = { password = "password"
                                             passwords = [] }
    
    let unlock (pm: PasswordManager<Locked>) (input: string) : Result<PasswordManager<Unlocked>, string> =
        if input = pm.password then
            Ok { password = pm.password
                 passwords = [] }
        else
            Error "Incorrect password"
            
    let addPassword (pm: PasswordManager<Unlocked>) (newPassword: string) : PasswordManager<Unlocked> =
        { pm with passwords = newPassword :: pm.passwords }


[<EntryPoint>]
let main argv =
    let pm = PasswordManager.create
    let unlockedPmResult = PasswordManager.unlock pm "password"
    match unlockedPmResult with
    | Ok unlockedPm ->
        let updatedPm = PasswordManager.addPassword unlockedPm "myNewPassword"
        printfn $"Password added successfully. Current passwords: {updatedPm.passwords}"
        1
    | Error errMsg ->
        printfn $"Failed to unlock password manager: {errMsg}"
        0 
    
    